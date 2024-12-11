import { Button, Form, Input, InputNumber, message, Select, Space, Upload } from 'antd'
import TextArea from 'antd/es/input/TextArea'
import { LeftOutlined, UploadOutlined } from '@ant-design/icons';
import { FormProps, useNavigate, useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { AnimalTypeModel } from '../models/AnimalTypeModel';
import { AnimalGenderModel } from '../models/AnimalGenderModel';
import { AnimalGenderOption } from '../models/AnimalGenderOption';
import { AnimalTypeOption } from '../models/AnimalTypeOption';
import axios from 'axios';
import { AnimalFormField } from '../models/AnimalFormField';

const animalsApi = import.meta.env.VITE_ANIMALS_API;
const typesApi = import.meta.env.VITE_ANIMAL_TYPES_API;
const gendersApi = import.meta.env.VITE_ANIMAL_GENDERS_API;

const normalFile = (f: any) => {
    return f?.file.originFileObj;
};

function ToNormalUrl(url: string | undefined) : string | undefined {
    if (!url) {
        return undefined;
    }

    const resUrl = url.startsWith("http") ? url : import.meta.env.VITE_ANIMALS_HOST + url.replace(/\\/g, "/")

    return resUrl;
}

export default function EditAnimal() {
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(true);
    const [animalTypes, setAnimalTypes] = useState<AnimalTypeOption[]>([]);
    const [animalGenders, setAnimalGenders] = useState<AnimalGenderOption[]>([]);
    const {id} = useParams();
    const [form] = Form.useForm<AnimalFormField>();
    const [imageUrl, setImageUrl] = useState<string>();
    const [imageName, setImageName] = useState<string>();
    
    useEffect(() => {
        fetch(typesApi)
        .then(res => res.json())
        .then(data => {
            setAnimalTypes((data as AnimalTypeModel[]).map(x => {
                return { label: x.name, value: x.id };
            }));
        });

        fetch(gendersApi)
        .then(res => res.json())
        .then(data => {
            setAnimalGenders((data as AnimalGenderModel[]).map(x => {
                return { label: x.name, value: x.id };
            }));
        });

        axios.get(animalsApi + id).then(res => {
            const data: AnimalFormField = res.data;

            if (data.imageUrl) {
                data.image = {
                    uid: "-1",
                    name: "default_img.png",
                    status: "done",
                    url: ToNormalUrl(data.imageUrl)
                };
                setImageUrl(data.imageUrl);
                setImageName(ToNormalUrl(data.imageUrl)!.split("/").pop());
            }
            

            form.setFieldsValue(data);
            setIsLoading(false);
        });

    }, []);
    
    const onSubmit: FormProps<AnimalFormField>["onFinish"] = (animal) => {
        const animalWithImgUrl = {
            ...animal,
            imageUrl
        }

        const formData = new FormData();
        for (const key in animalWithImgUrl) {
            if (key === "image" && animalWithImgUrl.image?.[0]?.originFileObj) {
                formData.append(key, animalWithImgUrl.image[0].originFileObj);
            } else {
                formData.append(key, animalWithImgUrl[key]);
            }
        }
        console.log(formData);

        axios.put(animalsApi, formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        }).then(res => {
            if (res.status == 200) {
                message.success("Animal edited succesfully!");
                navigate("/animals");
            }
            else {
                message.error("Something went wrong!");
            }
        });
    }

    return (
        <div>
            <Button onClick={() => navigate(-1)} color="default" variant="text" icon={<LeftOutlined />}></Button>

            <h2>Edit Animal</h2>
            
            {isLoading ? <p>Loading...</p> :
            <Form
                form={form}
                labelCol={{
                    span: 2,
                }}
                wrapperCol={{
                    span: 10,
                }}
                layout="horizontal"
                onFinish={onSubmit}
            >
                <Form.Item hidden name="id"></Form.Item>
                <Form.Item<AnimalFormField> label="Name" name="name"
                    rules={[
                        {
                            required: true,
                            message: "Field must be filled"
                        }
                    ]}>
                    <Input />
                </Form.Item>
                <Form.Item<AnimalFormField> label="Animal type" name="animalTypeId" rules={[
                    {
                        required: true,
                        message: "Field must be filled"
                    }
                ]}>
                    <Select options={animalTypes}></Select>
                </Form.Item>
                <Form.Item<AnimalFormField> label="Animal gender" name="genderId" rules={[
                    {
                        required: true,
                        message: "Field must be filled"
                    }
                ]}>
                    <Select options={animalGenders}></Select>
                </Form.Item>
                <Form.Item<AnimalFormField> label="Months" name="months" rules={[
                    {
                        required: true,
                        message: "Field must be filled"
                    }
                ]}>
                    <InputNumber style={{ width: '100%' }} />
                </Form.Item>

                <Form.Item<AnimalFormField> label="Image" name="image" valuePropName='file' getValueFromEvent={normalFile} rules={[
                    {
                        required: true,
                        message: "Img must be added"
                    }
                ]}>
                    <Upload maxCount={1}
                    listType='picture-card'
                        defaultFileList={[
                            {
                                uid: "-1",
                                name: imageName,
                                status: "done",
                                url: ToNormalUrl(imageUrl),
                            }
                        ]}
                    >
                        <Button icon={<UploadOutlined/>}></Button>
                    </Upload>
                </Form.Item>
                <Form.Item<AnimalFormField> label="Description" name="description">
                    <TextArea rows={4} />
                </Form.Item>
                <Form.Item
                    wrapperCol={{
                        offset: 4,
                        span: 16,
                    }}
                >
                    <Space>
                        <Button type="default" htmlType="reset" onClick={() => navigate(-1)}>
                            Cancel
                        </Button>
                        <Button type="primary" htmlType="submit">
                            Edit
                        </Button>
                    </Space>
                </Form.Item>
            </Form>
            }
        </div>
    )
}