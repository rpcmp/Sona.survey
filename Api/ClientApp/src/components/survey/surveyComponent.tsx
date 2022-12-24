import React, { useState } from 'react';
import { useFormik, FormikErrors } from 'formik';
import { Card, CardBody, CardHeader, CardFooter, Button, Form, FormGroup, Input, Label, FormFeedback } from 'reactstrap';
import { SurveyDto, AppointmentType, WalkingToesType, CureType, SuccessTherapyType } from './surveyModels';
import { TextModalProps } from '../models';

export default function SurveyComponent(props: TextModalProps) {
    const formik = useFormik<SurveyDto>({
        initialValues: {
            firstName: '',
            middleName: '',
            lastName: '',
            birthdate: '',
            appointment: null,
            walkingToes: null,
            cure: null,
            successTherapy: null
        },
        validate: validate,
        onSubmit: submit
    });

    async function validate(value: SurveyDto): Promise<FormikErrors<SurveyDto>> {
        let errors: FormikErrors<SurveyDto> = {};

        if (value.appointment == AppointmentType.First) {
            await formik.setFieldValue('cure', '', false);
            await formik.setFieldValue('successTherapy', '', false);
        }

        if (value.appointment == AppointmentType.NotFirst) {
            await formik.setFieldValue('walkingToes', '', false);
        }

        if (formik.submitCount == 0) {
            return errors;
        }

        if (isEmpty(value.firstName)) {
            errors.firstName = 'Необходимо указать имя ребенка';
        }

        if (isEmpty(value.lastName)) {
            errors.lastName = 'Необходимо указать фамилию ребенка';
        }

        if (isEmpty(value.birthdate)) {
            errors.birthdate = 'Необходимо указать дату рождения ребенка';
        }

        let radioError = 'Необходимо выбрать необходимый вариант ответа';

        if (isEmpty(value.appointment)) {
            errors.appointment = radioError;
        }

        if (value.appointment == AppointmentType.First && isEmpty(value.walkingToes)) {
            errors.walkingToes = radioError;
        }

        if (value.appointment == AppointmentType.NotFirst && isEmpty(value.cure)) {
            errors.cure = radioError;
        }

        if (value.appointment == AppointmentType.NotFirst && !isEmpty(value.cure) && isEmpty(value.successTherapy)) {
            errors.successTherapy = radioError;
        }

        return errors;
    }

    function isEmpty(value: string | null | undefined): boolean {
        return value == undefined || value == null || value.trim().length == 0;
    }

    async function submit(value: SurveyDto): Promise<any> {
        let errors = await formik.validateForm();
        let error = Object.values(errors).every(x => isEmpty(x));

        if (!error) {
            return;
        }

        if (formik.values.appointment == AppointmentType.First) {
            if (formik.values.walkingToes == WalkingToesType.MoreThen50) {
                props.setModalText("Рекомендовано ношение пирамидальных стелек на 6-8 недель. Стельки носить постоянно, включая занятия спортом и время, проведённое дома. Провести обследование через 8 недель.");
            } else {
                props.setModalText("Рекомендована выжидательная тактика (отложить начало терапии на 3-6 месяцев), затем снова провести обследование и пригласить на повторный прием через 6 месяцев");
            }
        }

        if (formik.values.appointment == AppointmentType.NotFirst && formik.values.cure == CureType.Night) {
            if (formik.values.successTherapy == SuccessTherapyType.Success) {
                props.setModalText("конец терапии. Ребенок наблюдается в плановом порядке специалистами.");
            } else {
                props.setModalText("изготовление и использование индивидуальных ночных шин в течение 6-10 недель. Повторный осмотр через 10 недель.");
            }
        }

        if (formik.values.appointment == AppointmentType.NotFirst && formik.values.cure == CureType.Pyramidal) {
            if (formik.values.successTherapy == SuccessTherapyType.Success) {
                props.setModalText("конец терапии. Ребенок наблюдается в плановом порядке специалистами.");
            } else {
                props.setModalText("ботулинотерапия в течение 12 недель. С последующим осомтром через 12 недель. Если эффекта нет - оперативное вмешательство (перкутанная миофасциотомия)");
            }
        }

        props.setIsOpen(true);

        return;
    }

    return (
        <Form onSubmit={formik.handleSubmit}>
            <Card className='mt-5'>
                <CardHeader>Ведение детей с ходьбой на носках</CardHeader>
                <CardBody>
                    <FormGroup>
                        <Label for='firstName'>Имя</Label>
                        <Input
                            id='firstName'
                            name='firstName'
                            type='text'
                            invalid={!isEmpty(formik.errors.firstName)}
                            value={formik.values.firstName}
                            onChange={formik.handleChange}
                        />
                        <FormFeedback valid={false}>{formik.errors.firstName}</FormFeedback>
                    </FormGroup>
                    <FormGroup>
                        <Label for='lastName'>Фамилия</Label>
                        <Input
                            id='lastName'
                            name='lastName'
                            type='text'
                            invalid={!isEmpty(formik.errors.lastName)}
                            value={formik.values.lastName}
                            onChange={formik.handleChange}
                        />
                        <FormFeedback valid={false}>{formik.errors.lastName}</FormFeedback>
                    </FormGroup>
                    <FormGroup>
                        <Label for='middleName'>Отчество</Label>
                        <Input
                            id='middleName'
                            name='middleName'
                            type='text'
                            invalid={!isEmpty(formik.errors.middleName)}
                            value={formik.values.middleName}
                            onChange={formik.handleChange}
                        />
                        <FormFeedback valid={false}>{formik.errors.middleName}</FormFeedback>
                    </FormGroup>
                    <FormGroup>
                        <Label for="birthdate">Дата рождения</Label>
                        <Input
                            id='birthdate'
                            name="birthdate"
                            type="date"
                            min={"1990-01-01"}
                            invalid={!isEmpty(formik.errors.birthdate)}
                            value={formik.values.birthdate}
                            onChange={formik.handleChange}
                        />
                        <FormFeedback valid={false}>{formik.errors.birthdate}</FormFeedback>
                    </FormGroup>
                    <FormGroup>
                        <Label>Первичный или повторный прием</Label>
                        <FormGroup check>
                            <Input
                                id='appointment'
                                name='appointment'
                                type='radio'
                                invalid={!isEmpty(formik.errors.appointment)}
                                value={AppointmentType.First}
                                onChange={formik.handleChange}
                            />
                            <Label check>Первичный прием</Label>
                        </FormGroup>
                        <FormGroup check>
                            <Input
                                id='appointment'
                                name='appointment'
                                type='radio'
                                invalid={!isEmpty(formik.errors.appointment)}
                                value={AppointmentType.NotFirst}
                                onChange={formik.handleChange}
                            />
                            <Label check>Повторный прием</Label>
                            <FormFeedback valid={false}>{formik.errors.appointment}</FormFeedback>
                        </FormGroup>
                    </FormGroup>

                    <FormGroup hidden={formik.values.appointment != AppointmentType.First}>
                        <Label>
                            Необходима беседа с родителями и обследование ребенка <b>у невролога и ортопеда, при исключении соматической патологии</b><br />
                            Диагноз: <b>генетическая (идиопатическая) ходьба на носках</b>
                        </Label>
                        <Label>Ходит ли ребенок на носках более 50% в день?</Label>
                        <FormGroup check>
                            <Input
                                id='walkingToes'
                                name='walkingToes'
                                type='radio'
                                invalid={!isEmpty(formik.errors.walkingToes)}
                                value={WalkingToesType.MoreThen50}
                                checked={formik.values.walkingToes == WalkingToesType.MoreThen50}
                                onChange={formik.handleChange}
                            />
                            <Label check>Ходьба на носках более 50% в день и угол дорсифлексии более 0</Label>
                        </FormGroup>
                        <FormGroup check>
                            <Input
                                id='walkingToes'
                                name='walkingToes'
                                type='radio'
                                invalid={!isEmpty(formik.errors.walkingToes)}
                                checked={formik.values.walkingToes == WalkingToesType.LessThen50}
                                value={WalkingToesType.LessThen50} onChange={formik.handleChange}
                            />
                            <Label check>Ходьба на носках менее 50% в день и угол дорсифлексии менее 0</Label>
                            <FormFeedback valid={false}>{formik.errors.walkingToes}</FormFeedback>
                        </FormGroup>
                    </FormGroup>

                    <FormGroup hidden={formik.values.appointment != AppointmentType.NotFirst}>
                        <Label>Какое лечение было ранее?</Label>
                        <FormGroup check>
                            <Input
                                id='cure'
                                name='cure'
                                type='radio'
                                invalid={!isEmpty(formik.errors.cure)}
                                value={CureType.Pyramidal}
                                checked={formik.values.cure == CureType.Pyramidal}
                                onChange={formik.handleChange}
                            />
                            <Label check>Пирамидальные стельки</Label>
                        </FormGroup>
                        <FormGroup check>
                            <Input
                                id='cure'
                                name='cure'
                                type='radio'
                                invalid={!isEmpty(formik.errors.cure)}
                                value={CureType.Night}
                                checked={formik.values.cure == CureType.Night}
                                onChange={formik.handleChange}
                            />
                            <Label check>Ночные шины</Label>
                            <FormFeedback valid={false}>{formik.errors.cure}</FormFeedback>
                        </FormGroup>
                    </FormGroup>

                    <FormGroup hidden={formik.values.appointment != AppointmentType.NotFirst || isEmpty(formik.values.cure)}>
                        <Label>{formik.values.cure == CureType.Pyramidal
                            ? "Ношение пирамидальных стелек оказалось эффективно, ребенок перестал ходить на носках?"
                            : "Ношение ночных шин оказалось эффективно?"}
                        </Label>
                        <FormGroup check>
                            <Input
                                id='successTherapy'
                                name='successTherapy'
                                type='radio'
                                invalid={!isEmpty(formik.errors.successTherapy)}
                                value={SuccessTherapyType.Success}
                                checked={formik.values.successTherapy == SuccessTherapyType.Success}
                                onChange={formik.handleChange}
                            />
                            <Label>Да</Label>
                        </FormGroup>
                        <FormGroup check>
                            <Input
                                id='successTherapy'
                                name='successTherapy'
                                type='radio'
                                invalid={!isEmpty(formik.errors.successTherapy)}
                                value={SuccessTherapyType.NotSuccess}
                                checked={formik.values.successTherapy == SuccessTherapyType.NotSuccess}
                                onChange={formik.handleChange}
                            />
                            <Label>Нет</Label>
                            <FormFeedback valid={false}>{formik.errors.successTherapy}</FormFeedback>
                        </FormGroup>
                    </FormGroup>
                </CardBody>
                <CardFooter>
                    <Button color="primary" type='submit'>Отправить</Button>
                </CardFooter>
            </Card>
        </Form>
    )
}